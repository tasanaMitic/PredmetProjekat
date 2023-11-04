import { Button, Container } from "react-bootstrap";
import PropTypes from 'prop-types';
import ProductTable from "../ProductTable";
import { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { getProducts, getStockedProducts } from "../../api/methods";

const ProductsPage = ({ user }) => {
    const history = useHistory();

    const [products, setProducts] = useState(null);

    useEffect(() => {
        switch(user.role){
            case "Admin":
                getProducts().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then((data) => {
                    setProducts(data);
                }).catch(err => {
                    setProducts(null);
                });
            break;
            case "Employee":
                getStockedProducts().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then((data) => {
                    setProducts(data);
                }).catch(err => {
                    setProducts(null);
                });
                break;
            default:
                break;
        }
        
    }, [user.role]);

    const handleClick = () => {
        history.replace('/addproduct');
    }


    return (
        <Container>
            {user.role === "Admin" && <Container className="d-grid gap-2">
                <h1>Products</h1>
                <Button variant="outline-dark" onClick={handleClick} >Add products</Button>
                <h2>Stocked products</h2>
                <ProductTable products={products} user={user}></ProductTable>
            </Container>
            }
            {user.role === "Employee" && <Container>
                <h1>Products</h1>
                <ProductTable products={products} user={user}></ProductTable>
            </Container>
            }
        </Container>

    );
}

ProductsPage.propTypes = {
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default ProductsPage;
import { Button, Container } from "react-bootstrap";
import PropTypes from 'prop-types';
import ProductTable from "../ProductTable";
import { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { getProducts, getStockedProducts, getProductTypes } from "../../api/methods";
import ProductTypeTable from "../ProductTypeTable";

const ProductsPage = ({ user }) => {
    const history = useHistory();

    const [products, setProducts] = useState(null);
    const [productTypes, setProductTypes] = useState(null);

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

                getProductTypes().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then((data) => {
                    setProductTypes(data);
                }).catch(err => {
                    setProductTypes(null);
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

    const handleClickProducts = () => {
        history.replace('/addproduct');
    }

    const handleClickProductTypes = () => {
        history.replace('/addproducttype');
    }

    return (
        <Container>
            {user.role === "Admin" && <Container className="d-flex flex-column align-items-center p-3">
                <h1>Products</h1>
                <Button onClick={handleClickProducts} >Add products</Button>
                <Button onClick={handleClickProductTypes} >Add product types</Button>
                <h3>All products</h3>
                <ProductTable products={products} user={user}></ProductTable>
                <h3>All product types</h3>
                <ProductTypeTable productTypes={productTypes} user={user}></ProductTypeTable>
            </Container>
            }
            {user.role === "Employee" && <Container>
                <h1>Stocked products</h1>
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
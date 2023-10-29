import { Button, Container } from "react-bootstrap";
import PropTypes from 'prop-types';
import ProductTable from "../ProductTable";
import { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { getProducts } from "../../api/methods";

const ProductsPage = ({ user }) => {
    const history = useHistory();

    const [products, setProducts] = useState(null);

    useEffect(() => {
        getProducts().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then((data) => {
            setProducts(data);
        }).catch(err => {
            console.log(err);
        })
    }, []);

    const handleClick = () => {
        history.replace('/addproduct'); 
    }


    return (
        <Container>
            <h1>Products</h1>
            <Button variant="outline-dark" onClick={handleClick}>Add products</Button>
            <ProductTable products={products} user={user}></ProductTable>
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
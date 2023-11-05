import { Button, Container, Table } from "react-bootstrap";
import { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import ModalCheck from './modals/ModalCheck'
import { deleteProduct } from "../api/methods";
import ModalError from "./modals/ModalError";
import ModalStock from "./modals/ModalStockProducts";

const ProductTable = ({ products, user }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [checkModal, setCheckModal] = useState(false);
    const [stockModal, setStockModal] = useState(false);
    const [productToDelete, setProductToDelete] = useState(null);
    const [productIdToStock, setProductIdToStock] = useState(null);

    useEffect(() => {
        setData(products);
    }, [products]);


    const handleStock = (productId) => {
        setProductIdToStock(productId);
        setStockModal(true);
    }

    const handleDelete = (productId) => {
        setProductToDelete(productId);
        setCheckModal(true);
    }

    const confirmDelete = () => {
        deleteProduct(productToDelete).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(data);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })
    }

    return (
        <Container>
        <ModalStock  setShow={setStockModal} show={stockModal} setError={setError} setErrorModal={setErrorModal} setData={setData} productId={productIdToStock}/>
            {data && data.length > 0 ?
                <Container>
                    <ModalCheck setShow={setCheckModal} show={checkModal} confirm={confirmDelete} />
                    {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}

                    <Table striped hover>
                        <thead>
                            <tr>
                                <th>Product name</th>
                                <th>Season</th>
                                <th>Sex</th>
                                <th>Size</th>
                                <th>Quantity</th>
                                <th>Category</th>
                                <th>Brand</th>
                                {user.role === 'Admin' && <th></th> }
                                {user.role === 'Admin' && <th></th> }
                            </tr>
                        </thead>
                        <tbody>
                            {data.map((product) => (
                                <tr key={product.productId}>
                                    <td>{product.name}</td>
                                    <td>{product.season}</td>
                                    <td>{product.sex}</td>
                                    <td>{product.size}</td>
                                    <td>{product.quantity}</td>
                                    <td>{product.category.name}</td>
                                    <td>{product.brand.name}</td>
                                    {user.role === 'Admin' &&
                                        <td>
                                            <Button variant="dark" onClick={() => handleDelete(product.productId)}>Delete</Button>
                                        </td>
                                    }
                                    {user.role === 'Admin' &&
                                        <td>
                                            <Button variant="dark" onClick={() => handleStock(product.productId)}>Stock</Button>
                                        </td>
                                    }
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Container>
                :
                <h3>There are no available products!</h3>
            }
        </Container>

    );
}

ProductTable.propTypes = {
    products: PropTypes.arrayOf(PropTypes.shape({
        productId: PropTypes.string,
        name: PropTypes.string,
        season: PropTypes.string,
        sex: PropTypes.string,
        quantity: PropTypes.number,
        category: PropTypes.object, //todo
        brand: PropTypes.object //todo
    })),
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}
export default ProductTable;
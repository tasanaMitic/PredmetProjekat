import { Button, Container, Table } from "react-bootstrap";
import { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import ModalCheck from './modals/ModalCheck'
import { deleteProduct } from "../api/methods";
import ModalError from "./modals/ModalError";
import ModalStock from "./modals/ModalStockProducts";
import ModalPrice from "./modals/ModalPrice";
import ModalProductDetails from "./modals/ModalProductDetails";

const ProductTable = ({ products, user }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [checkModal, setCheckModal] = useState(false);
    const [stockModal, setStockModal] = useState(false);
    const [priceModal, setPriceModal] = useState(false);
    const [detailsModal, setDetailsModal] = useState(false);
    const [productToDelete, setProductToDelete] = useState(null);
    const [productIdToStock, setProductIdToStock] = useState(null);
    const [product, setProduct] = useState(null);

    useEffect(() => {
        setData(products);
    }, [products]);


    const handleStock = (productId) => {
        setProductIdToStock(productId);
        setStockModal(true);
    }

    const handlePrice = (product) => {
        setProduct(product);
        setPriceModal(true);
    }

    const handleDelete = (productId) => {
        setProductToDelete(productId);
        setCheckModal(true);
    }

    const handleClick = (product) => {
        setProduct(product);
        setDetailsModal(true);
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
        <Container className="d-flex flex-column align-items-center p-3">
            <ModalStock setShow={setStockModal} show={stockModal} setError={setError} setErrorModal={setErrorModal} setData={setData} productId={productIdToStock} />

            {data && data.length > 0 ?
                <Container>
                    {product && <ModalProductDetails setShow={setDetailsModal} show={detailsModal} product={product} />}
                    <ModalCheck setShow={setCheckModal} show={checkModal} confirm={confirmDelete} />
                    {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                    <Table striped hover>
                        <thead>
                            <tr>
                                <th>Product name</th>
                                <th>Product type</th>
                                <th>Quantity</th>
                                <th>Category</th>
                                <th>Brand</th>
                                <th>Price</th>
                                {user.role === 'Admin' && <th></th>}
                                {user.role === 'Admin' && <th></th>}
                                {user.role === 'Admin' && <th></th>}
                            </tr>
                        </thead>
                        <tbody>
                            {data.map((product) => (
                                <tr key={product.productId}>
                                    <td onClick={() => handleClick(product)}>{product.name}</td>
                                    <td onClick={() => handleClick(product)}>{product.productType.name}</td>
                                    <td onClick={() => handleClick(product)}>{product.quantity}</td>
                                    <td onClick={() => handleClick(product)}>{product.category.name}</td>
                                    <td onClick={() => handleClick(product)}>{product.brand.name}</td>
                                    <td onClick={() => handleClick(product)}>{product.price}$</td>
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
                                    {user.role === 'Admin' && product.price == 0 &&
                                        <td>
                                            <Button variant="dark" onClick={() => handlePrice(product)}>Set price</Button>
                                        </td>
                                    }
                                    {user.role === 'Admin' && product.price != 0 &&
                                        <td>
                                            <Button variant="dark" onClick={() => handlePrice(product)}>Change price</Button>
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
        quantity: PropTypes.number,
        price: PropTypes.number,
        category: PropTypes.shape({
            name: PropTypes.string,
            categoryId: PropTypes.string,
        }),
        brand: PropTypes.shape({
            name: PropTypes.string,
            brandId: PropTypes.string,
        }),
        attributeValues: PropTypes.arrayOf(PropTypes.shape({
            attributeId: PropTypes.string,
            attributeName: PropTypes.string,
            attributeValue: PropTypes.string,
        })),
        productType: PropTypes.shape({
            productTypeId: PropTypes.string,
            name: PropTypes.string,
            attributes: PropTypes.arrayOf(PropTypes.shape({
                attributeId: PropTypes.string,
                attributeName: PropTypes.string,
            }))
        })
    })),
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}
export default ProductTable;
import { Button, Container, Table } from "react-bootstrap";
import { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import ModalCheck from './modals/ModalCheck'
import { deleteProductType } from "../api/methods";
import ModalError from "./modals/ModalError";
import ModalProductTypeDetails from "./modals/ModalProductTypeDetails";

const ProductTypeTable = ({ productTypes }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [checkModal, setCheckModal] = useState(false);
    const [detailsModal, setDetailsModal] = useState(false);
    const [productToDelete, setProductToDelete] = useState(null);
    const [productType, setProduct] = useState(null);

    useEffect(() => {
        setData(productTypes);
    }, [productTypes]);

    const handleDelete = (productId) => {
        setProductToDelete(productId);
        setCheckModal(true);
    }

    const handleClick = (productType) => {
        setProduct(productType);
        setDetailsModal(true);
    }

    const confirmDelete = () => {
        deleteProductType(productToDelete).then(res => {
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
            {data && data.length > 0 ?
                <Container className="d-flex flex-column align-items-center p-3">
                    {productType && <ModalProductTypeDetails setShow={setDetailsModal} show={detailsModal} productType={productType} />}
                    <ModalCheck setShow={setCheckModal} show={checkModal} confirm={confirmDelete} />
                    {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                    <Table striped hover style={{ width: '30%' }}>
                        <thead>
                            <tr>
                                <th>Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            {data.map((productType) => (
                                <tr key={productType.productTypeId}>
                                    <td onClick={() => handleClick(productType)}>{productType.name}</td>
                                    <td style={{ textAlign: 'right' }}>
                                        <Button variant="dark" onClick={() => handleDelete(productType.productTypeId)}>Delete</Button>
                                    </td>

                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Container>
                :
                <h3>There are no available product types!</h3>
            }
        </Container>

    );
}

ProductTypeTable.propTypes = {
    productTypes: PropTypes.arrayOf(PropTypes.shape({
        name: PropTypes.string,
        productTypeId: PropTypes.string,
        attributes: PropTypes.arrayOf(PropTypes.shape({
            attributeId: PropTypes.string,
            attributeName: PropTypes.string,
        })),
    }))
}
export default ProductTypeTable;
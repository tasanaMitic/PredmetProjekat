import { Button, Container, Form, FormControl, Pagination, Table } from "react-bootstrap";
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
    const [searchTerm, setSearchTerm] = useState('');

    const [currentPageData, setCurrentPageData] = useState(null);
    const [pagination, setPagination] = useState({
        totalPages: 0,
        currentPage: 0,
        pageSize: 2
    });

    useEffect(() => {
        setData(productTypes);
        setPagination(prevPagination => ({
            ...prevPagination,
            totalPages: productTypes && productTypes.length > 0 ? Math.ceil(productTypes.length / pagination.pageSize) : 0,
            currentPage: productTypes && productTypes.length > 0 ? 1 : 0,
        }));
        setCurrentPageData(productTypes ? productTypes.slice(pagination.currentPage, pagination.pageSize + pagination.currentPage) : null);
    }, [productTypes]);

    const confirmDelete = () => {
        deleteProductType(productToDelete).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(data);
            setCurrentPageData(data ? data.slice((pagination.currentPage - 1) * pagination.pageSize, (pagination.currentPage - 1) * pagination.pageSize + pagination.pageSize) : null);
            setPagination(prevPagination => ({
                ...prevPagination,
                totalPages: data && data.length > 0 ? Math.ceil(data.length / pagination.pageSize) : 0,
            }));
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })
    }

    const handleDelete = (productId) => {
        setProductToDelete(productId);
        setCheckModal(true);
    }

    const handleClick = (productType) => {
        setProduct(productType);
        setDetailsModal(true);
    }
    const handlePageChange = (index) => {
        setPagination(prevPagination => ({
            ...prevPagination,
            currentPage: index,
        }));
        setCurrentPageData(data.slice((index - 1) * pagination.pageSize, (index - 1) * pagination.pageSize + pagination.pageSize));
    }

    const handleSearch = (value) => {
        setSearchTerm(value);

        if (value === '') {
            setPagination(prevPagination => ({
                ...prevPagination,
                totalPages: data && data.length > 0 ? Math.ceil(data.length / pagination.pageSize) : 0,
                currentPage: data && data.length > 0 ? 1 : 0,
            }));
            setCurrentPageData(data ? data.slice(0, pagination.pageSize + 0) : null)
        }
        else {
            var filteredData = data.filter(x => x.name.startsWith(value));
            setPagination(prevPagination => ({
                ...prevPagination,
                totalPages: filteredData && filteredData.length > 0 ? Math.ceil(filteredData.length / pagination.pageSize) : 0,
                currentPage: filteredData && filteredData.length > 0 ? 1 : 0,
            }));
            setCurrentPageData(filteredData ? filteredData.slice(0, pagination.pageSize + 0) : null);
        }
    };

    return (
        <Container className="d-flex flex-column align-items-center p-3">
            <Container style={{ width: '30%' }}>
                <Form>
                    <FormControl
                        type="text"
                        placeholder="Search"
                        className="mb-2 mt-2"
                        value={searchTerm}
                        onChange={(e) => handleSearch(e.target.value)} />
                </Form>
            </Container>
            {currentPageData && currentPageData.length > 0 ?
                <Container>
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
                                {currentPageData.map((productType) => (
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
                    <Container className="d-flex flex-column align-items-center">
                        <Pagination>
                            {Array.from({ length: pagination.totalPages }, (_, index) => (
                                <Pagination.Item key={index + 1}
                                    active={index + 1 === pagination.currentPage}
                                    onClick={() => handlePageChange(index + 1)}>
                                    {index + 1}
                                </Pagination.Item>
                            ))}
                        </Pagination>
                    </Container>
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
    })),
}
export default ProductTypeTable;
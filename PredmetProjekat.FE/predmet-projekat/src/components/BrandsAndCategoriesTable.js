import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import ModalCheck from './modals/ModalCheck';
import ModalError from "./modals/ModalError";
import PropTypes from 'prop-types';
import { deleteCategory, deleteBrand } from "../api/methods";

const BrandsAndCategoriesTable = ({ categories, brands }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [checkModal, setCheckModal] = useState(false);
    const [itemToDelete, setItemToDelete] = useState({ itemType: null, itemId: null });


    useEffect(() => {
        if (categories) {
            setData(categories);
        }
        else if (brands) {
            setData(brands);
        }
    }, [categories, brands]);

    const confirmDelete = () => {
        switch (itemToDelete.itemType) {
            case 'category':
                deleteCategory(itemToDelete.itemId).then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                })
                    .then(data => {
                        setData(data);
                    }).catch(err => {
                        setError(err.response);
                        setErrorModal(true);
                    })
                break;
            case 'brand':
                deleteBrand(itemToDelete.itemId).then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                })
                    .then(data => {
                        setData(data);
                    }).catch(err => {
                        setError(err.response);
                        setErrorModal(true);
                    })
                break;
            default:
                break;
        }
    }

    const handleDelete = (itemId) => {
        setCheckModal(true);
        if (categories) {
            setItemToDelete({ itemType: 'category', itemId: itemId });
        }
        else if (brands) {
            setItemToDelete({ itemType: 'brand', itemId: itemId });
        }
    }

    return (
        <Container>
            {data && data.length > 0 ?
                <Container className="d-flex flex-column align-items-center p-3">
                    {categories && <h1>Categories</h1>}
                    {brands && <h1>Brands</h1>}
                    {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                    <ModalCheck setShow={setCheckModal} show={checkModal} confirm={confirmDelete} />
                    <Table striped hover style={{ width: '50%' }}>                        
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories && data.map((categorie) => (
                                <tr key={categorie.categoryId}>
                                    <td key={categorie.name}>{categorie.name}</td>
                                    <td style={{ textAlign: 'right' }}>
                                        <Button variant="dark" onClick={() => handleDelete(categorie.categoryId)}>Delete</Button>
                                    </td>

                                </tr>
                            ))}
                            {brands && data.map((brand) => (
                                <tr key={brand.brandId}>
                                    <td key={brand.name}>{brand.name}</td>
                                    <td style={{ textAlign: 'right' }}>
                                        <Button variant="dark" onClick={() => handleDelete(brand.brandId)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>

                </Container>
                :
                <h3>There are no available {categories ? 'categories' : 'brands'}!</h3>
            }
        </Container>

    );
}

BrandsAndCategoriesTable.propTypes = {
    brands: PropTypes.arrayOf(PropTypes.shape({
        brandId: PropTypes.string,
        name: PropTypes.string
    })),
    categories: PropTypes.arrayOf(PropTypes.shape({
        categoryId: PropTypes.string,
        name: PropTypes.string
    }))
}

export default BrandsAndCategoriesTable;
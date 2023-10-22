import { Container, Button } from "react-bootstrap";
import { useState, useEffect } from "react";
import CategoriesTable from '../BrandsAndCategoriesTable'
import { getCategories } from '../../api/methods'
import ModalCategory from "../modals/ModalCategory";
import ModalError from "../modals/ModalError";

const CategoriesPage = () => {
    const [data, setData] = useState(null);
    const [categoryModal, setCategoryModal] = useState(false);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);

    useEffect(() => {
        getCategories().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(data);
        })
            .catch(err => {
                setError(err.response);
            })
    }, [data]);

    const handleClick = () => {
        setCategoryModal(true);
    }

    return (
        <Container className="d-fex p-3">
            <ModalCategory setShow={setCategoryModal} show={categoryModal} setError={setError} setErrorModal={setErrorModal} setData={setData}></ModalCategory>
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            <Button variant="outline-dark" onClick={() => handleClick()}>Add category</Button>
            <CategoriesTable categories={data} />
        </Container>
    );
}

export default CategoriesPage;
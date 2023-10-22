import { Container, Button } from "react-bootstrap";
import { useState, useEffect } from "react";
import BrandsTable from '../BrandsAndCategoriesTable'
import { getBrands } from '../../api/methods';
import ModalError from "../modals/ModalError";
import ModalBrand from "../modals/ModalBrand";

const BrandsPage = () => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [brandModal, setBrandModal] = useState(false);
    const [errorModal, setErrorModal] = useState(false);

    useEffect(() => {
        getBrands().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(data);
        })
            .catch(err => {
                setError(err);
            })
    }, [data]);

    const handleClick = () => {
        setBrandModal(true);
    }

    return (
        <Container className="d-fex p-3">
            <ModalBrand setShow={setBrandModal} show={brandModal} setError={setError} setErrorModal={setErrorModal} setData={setData}></ModalBrand>
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            <Button variant="outline-dark" onClick={() => handleClick()}>Add brand</Button>
            <BrandsTable brands={data} />
        </Container>
    );
}

export default BrandsPage;
import {  Container } from "react-bootstrap";
import CategoriesPage from "./CategoriesPage";
import BrandsPage from "./BrandsPage";

const BrandsAndCetegoriesPage = () => {
    return (
        <Container className="d-flex p-2" >
            <CategoriesPage />
            <BrandsPage />
        </Container>

    );
}

export default BrandsAndCetegoriesPage;
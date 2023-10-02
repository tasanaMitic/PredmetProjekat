import { Container } from "react-bootstrap";
import CategoriesPage from "./CategoriesPage";
import BrandsPage from "./BrandsPage";

function BrandsAndCetegoriesPage() {

    return (
        <Container>
        <CategoriesPage/>
        <BrandsPage/>
        </Container>
    );
}

export default BrandsAndCetegoriesPage;
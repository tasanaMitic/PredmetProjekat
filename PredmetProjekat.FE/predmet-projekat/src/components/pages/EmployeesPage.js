import { Button, Container } from "react-bootstrap";
import useFetch from "../useFetch";

function EmployeesPage() {
    const { data: employees, isPending, error } = useFetch('https://localhost:7155/api/category');


    return (
        <Container>
            <h1>Employees Page component</h1>
            {error && <div>{error}</div>}
            {isPending && <div>Loading..</div>}
            <div>Cao employees</div>
        </Container>
    );
}

export default EmployeesPage;
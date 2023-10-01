import { Button, Container } from "react-bootstrap";
import { useState } from "react";

function EmployeesPage() {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);


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
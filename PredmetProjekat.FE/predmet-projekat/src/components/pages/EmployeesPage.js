import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import UsersTable from '../UsersTable'
import { getEmployees } from '../../api/methods'

function EmployeesPage({ user }) {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getEmployees().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setData(data);
                setIsPending(false);
                setError(null);
            })
            .catch(err => {
                setIsPending(false);
                setError(err);
            })
    }, []);

    return (
        <Container>
            <h1>All Employees</h1>
            {error && <div>{error}</div>}
            {!isPending && <UsersTable users={data} loggedInUser={user} />}
        </Container>
    );
}

export default EmployeesPage;
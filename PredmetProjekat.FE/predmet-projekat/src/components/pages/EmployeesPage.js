import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import UsersTable from '../UsersTable';
import { getEmployees } from '../../api/methods';
import PropTypes from 'prop-types';

const EmployeesPage = ({user}) => {
    const [data, setData] = useState(null);
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
                setError(null);
            })
            .catch(err => {
                setError(err);  //todo
            })
    }, []);

    return (
        <Container>
            <h1>All Employees</h1>
            <UsersTable users={data} loggedInUser={user} />
        </Container>
    );
}

EmployeesPage.propTypes = {
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default EmployeesPage;
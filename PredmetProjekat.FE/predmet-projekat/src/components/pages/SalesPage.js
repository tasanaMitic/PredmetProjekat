import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import PropTypes from 'prop-types';
import { getSalesForUser } from "../../api/methods";

const SalesPage = ({user}) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        getSalesForUser().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(data);
            console.log(data);
        })
            .catch(err => {
                setError(err);
            });
    }, [data]);


    return (
        <Container>
            <h1>All Sales</h1>
        </Container>
    );
}

SalesPage.propTypes = {
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default SalesPage;
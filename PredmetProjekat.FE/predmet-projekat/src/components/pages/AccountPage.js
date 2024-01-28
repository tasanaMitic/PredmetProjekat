import { Container, Form, Button } from 'react-bootstrap';
import { useHistory } from "react-router-dom";
import PropTypes from 'prop-types';
import { useState, useEffect } from 'react';
import { getUser, updateUser } from '../../api/methods';
import ModalCheck from '../modals/ModalCheck';
import ModalSuccess from '../modals/ModalSuccess';
import ModalError from '../modals/ModalError';

const AccountPage = ({ user }) => {
    const history = useHistory();
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [checkModal, setCheckShow] = useState(false);
    const [errorModal, setErrorModal] = useState(false);
    const [successModal, setSuccessModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);

    useEffect(() => {
        getUser(user.role, user.username).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setData({
                    ...data,
                    email: data.email,
                    firstName: data.firstName,
                    lastName: data.lastName,
                    username: data.username
                });
            })
            .catch(err => {
                setError(err.response);
            })

    }, [user.username, user.role]);

    const handleSubmit = (e) => {
        e.preventDefault();
        setCheckShow(true);
    }

    const confirm = () => {
        updateUser(user.role, data).then(res => {
            if (res.status !== 204) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully updated your data!");
        })
            .catch(err => {
                setError(err.response);
                console.log(err);
                //setErrorModal(true);
            })

    }

    const clearData = () => {
        history.replace('/');
    }

    return (
        <Container className="d-flex flex-column align-items-center p-3">
            <h1>My Account</h1>
            {data &&
                <Form onSubmit={handleSubmit}>
                    <ModalCheck setShow={setCheckShow} show={checkModal} confirm={confirm} />
                    <ModalSuccess setShow={setSuccessModal} show={successModal} message={successMessage} clearData={clearData}></ModalSuccess>
                    {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                    <Form.Group className="custom-width mb-3" controlId="formBasicUserName">
                        <Form.Label>Username</Form.Label>
                        <Form.Control type="username" placeholder={data.username} value={data.username} disabled />
                    </Form.Group>
                    <Form.Group className="custom-width mb-3" controlId="formBasicFirstName">
                        <Form.Label>First name</Form.Label>
                        <Form.Control type="firstName" placeholder={data.firstName} value={data.firstName} onChange={(e) => setData({ ...data, firstName: e.target.value })} />
                    </Form.Group>
                    <Form.Group className="custom-width mb-3" controlId="formBasicLastName">
                        <Form.Label>Last name</Form.Label>
                        <Form.Control type="lastName" placeholder={data.lastName} value={data.lastName} onChange={(e) => setData({ ...data, lastName: e.target.value })} />
                    </Form.Group>
                    <Form.Group className="custom-width mb-3" controlId="formBasicEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control type="email" placeholder={data.email} value={data.email} onChange={(e) => setData({ ...data, email: e.target.value })} />
                    </Form.Group>
                    <Container className="d-flex flex-column align-items-center p-3">
                        <Button variant="outline-dark" type="submit">
                            Save changes
                        </Button>
                    </Container>
                </Form>
            }
        </Container>
    );
}

AccountPage.propTypes = {
    user: PropTypes.shape({
        username: PropTypes.string,
        role: PropTypes.string
    })
}

export default AccountPage;
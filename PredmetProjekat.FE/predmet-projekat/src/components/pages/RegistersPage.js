import { Container, Button, Table } from 'react-bootstrap';
import { useState, useEffect } from 'react';
import ModalError from '../modals/ModalError';
import ModalRegister from '../modals/ModalRegister';
import { getRegisters } from '../../api/methods';

const RegistersPage = () => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [registerModal, setRegisterModal] = useState(false);
    

    useEffect(() => {
        getRegisters().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setData(data);
            })
            .catch(err => {
                setData(null);
            })
    }, []);

    const handleClick = () => {
        setRegisterModal(true);
    }

    return (
        <Container>
            <h1>Registers</h1>
            <ModalRegister show={registerModal} setShow={setRegisterModal} setData={setData} setError={setError} setErrorModal={setErrorModal} ></ModalRegister>
            <Button variant="outline-dark" onClick={handleClick} >Add register</Button>
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            {data ? <Table striped hover >
                <thead>
                    <tr>
                        <th>Location</th>
                        <th>Register code</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((register) => (
                        <tr key={register.registerId}>
                            <td>{register.location}</td>
                            <td>{register.registerCode}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
            : <h3>There are no registers!</h3>}
        </Container>
    );
}

export default RegistersPage;
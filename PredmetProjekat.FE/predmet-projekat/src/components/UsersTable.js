import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import ModalCheck from './ModalCheck'
import { deleteEmployee } from "../api/methods";
import AlertDissmisable from "./Alert";

const AdminsTable = ({ admins, users }) => {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);
    const [check, setCheck] = useState(false);
    const [username, setUsername] = useState(null);

    useEffect(() => {
        if (admins) {
            setData(admins);
        }
        else if (users) {
            setData(users);
        }
    }, []);

    const handleClick = (username) => {
        console.log(username);
    }

    const handleDelete = (username) =>{
        setCheck(true);
        setUsername(username);
    }
    
    const confirmDelete = () => {
        deleteEmployee(username).then(res => {
            if (res.status !== 204) {
                throw Error('There was an error with the request!');
            }
            setData(data.filter(user => username !== user.username));
        })        
        .catch(err => {
            console.log("error")
            setIsPending(false);
            setError(err);
        })
    }


    return (
        <Container>
        {error && <AlertDissmisable error={error} setError={setError}/>}
            {data && <Table striped hover>
                <ModalCheck setShow={setCheck} show={check} confirm={confirmDelete} />
                <thead>
                    <tr>
                        <th>First name</th>
                        <th>Last name</th>
                        <th>Username</th>
                        <th>Email</th>
                        {users && <th></th>}
                        {users && <th></th>}
                    </tr>
                </thead>
                <tbody>
                    {data.map((user) => (
                        <tr key={user.username}>
                            <td>{user.firstName}</td>
                            <td>{user.lastName}</td>
                            <td>{user.username}</td>
                            <td>{user.email}</td>
                            {users &&
                                <td>
                                    <Button variant="dark" onClick={() => handleClick(user.username)}>Assign manager</Button>
                                </td>
                            }
                            {users &&
                                <td>
                                    <Button variant="dark" onClick={() => handleDelete(user.username)}>Delete</Button>
                                </td>
                            }
                        </tr>
                    ))}
                </tbody>
            </Table>
            }
        </Container>
    );
}

export default AdminsTable;
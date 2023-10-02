import { Button, Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import UsersTable from '../UsersTable'
import { getAdmins } from '../../api/methods'

function AdminsPage() {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        console.log('tasana');
        getAdmins().then(res => {
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
                console.log("error")
                setIsPending(false);
                setError(err);
            })
    }, []);

    


    return (
        <Container>        
            <h1>All Admins</h1>
            {error && <div>{error}</div>}
            {!isPending && <UsersTable admins={data} />}
        </Container>
    );
}

export default AdminsPage;
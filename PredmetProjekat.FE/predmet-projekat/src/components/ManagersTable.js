import { useEffect, useState } from "react";
import { Container, Table } from "react-bootstrap";

const ManagersTable = ({ managers, loggedInUser, setSelectedManager, selectedManager, selectedUser }) => {
    const [data, setData] = useState(null);

    useEffect(() => {
        setData(managers.filter(manager => manager.username !== selectedUser));
    }, []);

    const highlightColor = (username) => {
        if(username === selectedManager){
            return "silver";
        }
        if (username === loggedInUser.username) {
            return "#FFE4E1";
        }
    }
    
    const handleClick = (username) => {
        setSelectedManager(username);
    }

    return (
        <Container>
            {data && <Table striped hover>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Username</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((user) => (
                        <tr key={user.username} onClick={() => handleClick(user.username)}>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.firstName} {user.lastName}</td>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.username}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
            }
        </Container>
    );
}

export default ManagersTable;
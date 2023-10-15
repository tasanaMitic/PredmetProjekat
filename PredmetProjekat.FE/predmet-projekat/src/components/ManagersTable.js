import { useEffect, useState } from "react";
import { Container, Table } from "react-bootstrap";
import PropTypes from 'prop-types';

const ManagersTable = ({ managers, loggedInUser, setSelectedManager, selectedManager}) => {
    const highlightColor = (username) => {
        if (username === selectedManager) {
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
            {managers && <Table striped hover>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Username</th>
                    </tr>
                </thead>
                <tbody>
                    {managers.map((user) => (
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

ManagersTable.propTypes = {
    managers: PropTypes.arrayOf(PropTypes.shape({
        email: PropTypes.string,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        username: PropTypes.string,
        managerId: PropTypes.string,
        manager: PropTypes.object   //todo
    })),
    loggedInUser: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    }),
    setSelectedManager: PropTypes.func,
    selectedManager: PropTypes.string
}

export default ManagersTable;
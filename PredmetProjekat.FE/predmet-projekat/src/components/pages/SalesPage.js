import { Container, Table } from "react-bootstrap";
import { useState, useEffect } from "react";
import PropTypes from 'prop-types';
import moment from 'moment';
import { getAllSales, getSalesForUser } from "../../api/methods";
import ModalSaleDetails from "../modals/ModalSaleDetails";

const SalesPage = ({ user }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [selectedSale, setSelectedSale] = useState(null);
    const [detailsModal, setDetailsModal] = useState(false);

    useEffect(() => {
        switch (user.role) {
            case "Employee":
                getSalesForUser().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then(data => {
                    const sortedData = [...data].sort((a, b) => new Date(a.date) - new Date(b.date));
                    setData(sortedData);
                }).catch(err => {
                    setError(err);
                });
                break;
            case "Admin":
                getAllSales().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then(data => {
                    const sortedData = [...data].sort((a, b) => new Date(a.date) - new Date(b.date));
                    setData(sortedData);
                })
                    .catch(err => {
                        setError(err);
                    });
                break;
            default:
                break;
        }
    }, [user.role]);

    const handleClick = (receiptId) => {
        setSelectedSale(data.find(x => x.receiptId === receiptId));
        setDetailsModal(true);
    }

    return (
        <Container>
            <h1>All Sales</h1>
            {data ?
                <Container>
                 {selectedSale  && <ModalSaleDetails setShow={setDetailsModal} show={detailsModal} sale={selectedSale}/>}
                <Table striped hover>
                <thead>
                    <tr>
                        {user.role === 'Admin' && <th>Employee </th>}
                        <th>Date</th>
                        <th>Location</th>
                        <th>Register</th>
                        <th style={{ fontWeight: 'bold' }}>Total price</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((sale) => (
                        <tr onClick={() => handleClick(sale.receiptId)} key={sale.receiptId}>
                            {user.role === 'Admin' && <td>{sale.soldBy.username}</td>}
                            <td>{moment(sale.date).format('DD/MM/YYYY')}</td>
                            <td>{sale.register.location}</td>
                            <td>{sale.register.registerCode}</td>
                            <td>{Math.round(sale.totalPrice * 100) / 100}$</td>
                        </tr>
                    ))}
                    <tr >
                        <td style={{ backgroundColor: "#FFE4E1" }}>Total price:</td>
                        {user.role === 'Admin' && <td style={{ backgroundColor: "#FFE4E1" }}></td>}
                        <td style={{ backgroundColor: "#FFE4E1" }}></td>
                        <td style={{ backgroundColor: "#FFE4E1" }}></td>
                        <td style={{ backgroundColor: "#FFE4E1",  fontWeight: 'bold'  }}>{Math.round(data.reduce((totalPrice, sale) => totalPrice + sale.totalPrice, 0) * 100) / 100}$</td>
                    </tr>
                </tbody>
            </Table>
                </Container>
                
                : <h3>There have been no sales yet!</h3>}
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
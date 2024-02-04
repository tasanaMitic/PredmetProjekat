import { Button, Col, Container, Dropdown, Form, Row, Table } from "react-bootstrap";
import { useState, useEffect } from "react";
import PropTypes from 'prop-types';
import moment from 'moment';
import { getAllSales, getFilteredSales, getSalesForUser } from "../../api/methods";
import ModalSaleDetails from "../modals/ModalSaleDetails";

const SalesPage = ({ user }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [selectedSale, setSelectedSale] = useState(null);
    const [detailsModal, setDetailsModal] = useState(false);

    const [employees, setEmployees] = useState([]);
    const [registers, setRegisters] = useState([]);
    const [dates, setDates] = useState([]);

    const [selectedItems, setSelectedItems] = useState({
        employees: [],
        dates: [],
        registers: [],
    });

    useEffect(() => {
        switch (user.role) {
            case "Employee":
                getSalesForUser().then(res => {
                    if (res.status !== 200) {
                        throw Error('There was an error with the request!');
                    }
                    return res.data;
                }).then(data => {
                    setData(sortData(data));
                    setRegisters(extractRegisters(data));
                    setDates(extractDates(data));
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
                    setData(sortData(data));
                    setEmployees(extractEmployees(data));
                    setRegisters(extractRegisters(data));
                    setDates(extractDates(data));
                })
                    .catch(err => {
                        setError(err);
                    });
                break;
            default:
                break;
        }
    }, [user.role]);

    const handleCheckboxChange = (key, item) => {
        setSelectedItems(prevState => {
            if (prevState[key].includes(item)) {
                return {
                    ...prevState,
                    [key]: prevState[key].filter(e => e !== item),
                };
            } else {
                return {
                    ...prevState,
                    [key]: [...prevState[key], item],
                };
            }
        });

    };

    const createString = (name, values) => {
        return values.length > 0 ? `&${name}=${values.join('|')}` : '';
    };

    const handleFilter = () => {
        const saleDates = createString('SaleDates', selectedItems.dates);
        const usernames = createString('EmployeeUsernames', selectedItems.employees);
        const registerCodes = createString('RegisterCodes', selectedItems.registers);

        const queryParams = `${saleDates}${usernames}${registerCodes}`;
        const formattedQueryParams = queryParams.length > 0 ? `?${queryParams.slice(1)}` : '';

        getFilteredSales(formattedQueryParams).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {
            setData(sortData(data));
            setEmployees(extractEmployees(data));
            setRegisters(extractRegisters(data));
            setDates(extractDates(data));
        })
            .catch(err => {
                setError(err);
            });
    }

    const sortData = (data) => {
        return [...data].sort((a, b) => new Date(a.date) - new Date(b.date));
    }

    const extractEmployees = (data) => {
        return [...new Set(data.map(item => item.soldBy.username))];
    }

    const extractLocation = (data) => {
        return [...new Set(data.map(item => item.register.location))];
    }

    const extractRegisters = (data) => {
        return [...new Set(data.map(item => item.register.registerCode))];
    }

    const extractDates = (data) => {
        return [...new Set(data.map(item => moment(item.date).format('DD/MM/YYYY')))];
    }

    const handleClick = (receiptId) => {
        setSelectedSale(data.find(x => x.receiptId === receiptId));
        setDetailsModal(true);
    }

    return (
        <Container className="d-flex flex-column align-items-center p-3">
            <h1>All Sales</h1>
            {data ?
                <Container className="d-flex ">
                    {selectedSale && <ModalSaleDetails setShow={setDetailsModal} show={detailsModal} sale={selectedSale} />}
                    <Container className="d-flex flex-column align-items-center" style={{ width: '20%' }} >
                        { user.role === 'Admin' &&<Dropdown>
                            <Dropdown.Toggle variant="white" id="dropdown-basic" className="w-2">
                                Select Employee
                            </Dropdown.Toggle>
                            <Dropdown.Menu style={{ alignItems: 'center' }}>
                                {employees.map((option) => (
                                    <div key={option} style={{ marginLeft: '10px' }}>
                                        <Form.Check
                                            type="checkbox"
                                            label={option}
                                            style={{ margin: 'auto' }}
                                            checked={selectedItems.employees.includes(option)}
                                            onChange={() => handleCheckboxChange('employees', option)} />
                                    </div>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>}
                        <Dropdown>
                            <Dropdown.Toggle variant="white" id="dropdown-basic" className="w-2">
                                Select Register
                            </Dropdown.Toggle>
                            <Dropdown.Menu style={{ alignItems: 'center' }}>
                                {registers.map((option) => (
                                    <div key={option} style={{ marginLeft: '10px' }}>
                                        <Form.Check
                                            type="checkbox"
                                            label={option}
                                            style={{ margin: 'auto' }}
                                            checked={selectedItems.registers.includes(option)}
                                            onChange={() => handleCheckboxChange('registers', option)} />
                                    </div>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>
                        <Dropdown>
                            <Dropdown.Toggle variant="white" id="dropdown-basic" className="w-2">
                                Select Date
                            </Dropdown.Toggle>
                            <Dropdown.Menu style={{ alignItems: 'center' }}>
                                {dates.map((option) => (
                                    <div key={option} style={{ marginLeft: '10px' }}>
                                        <Form.Check
                                            type="checkbox"
                                            label={option}
                                            style={{ margin: 'auto' }}
                                            checked={selectedItems.dates.includes(option)}
                                            onChange={() => handleCheckboxChange('dates', option)} />
                                    </div>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>
                        <Button variant="outline-dark" style={{ width: '70%' }} onClick={handleFilter}>Apply filter</Button>
                    </Container>

                    <Table striped hover >
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
                                <td style={{ backgroundColor: "#FFE4E1", fontWeight: 'bold' }}>{Math.round(data.reduce((totalPrice, sale) => totalPrice + sale.totalPrice, 0) * 100) / 100}$</td>
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
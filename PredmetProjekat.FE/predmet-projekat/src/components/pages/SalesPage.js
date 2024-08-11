import { Button, Container, Dropdown, Form, Table, Row, Col } from "react-bootstrap";
import { useState, useEffect } from "react";
import DatePicker from "react-datepicker";
import PropTypes from 'prop-types';
import moment from 'moment';
import { getFilteredSales } from "../../api/methods";
import ModalSaleDetails from "../modals/ModalSaleDetails";
import "react-datepicker/dist/react-datepicker.css";

const startDate = 'startDate';
const endDate = 'endDate';

const SalesPage = ({ user }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [validationErrors, setValidationErrors] = useState({ startDate: '', endDate: '' });
    const [selectedSale, setSelectedSale] = useState(null);
    const [detailsModal, setDetailsModal] = useState(false);

    const [employees, setEmployees] = useState([]);
    const [registers, setRegisters] = useState([]);
    const [locations, setLocations] = useState([]);

    const [selectedItems, setSelectedItems] = useState({  
        employees: [],
        registers: [],
        locations: [],
        startDate: new Date().toISOString().split('T')[0],
        endDate: new Date().toISOString().split('T')[0],
        price: 0
    });

    useEffect(() => {
        getFilteredSales(createRequestParameters()).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then(data => {          
            setData(sortData(data.receiptDtos));
            setEmployees(extractData(data.optionParameters.employeeUsernames));
            setRegisters(extractData(data.optionParameters.registerCodes));
            setLocations(extractData(data.optionParameters.locations));        
        }).catch(err => {
            setError(err);
        });
    }, [selectedItems]);

    const createRequestParameters = () => {
        const usernames = createString('EmployeeUsernames', selectedItems.employees);
        const registerCodes = createString('RegisterCodes', selectedItems.registers);
        const price = createString('Price', checkPrice(selectedItems.price));
        const locations = createString('Locations', selectedItems.locations);
        const startDate = createString('StartDate', checkDate(selectedItems.startDate));
        const endDate = createString('EndDate', checkDate(selectedItems.endDate));

        const queryParams = `${usernames}${registerCodes}${price}${locations}${startDate}${endDate}`;
        const formattedQueryParams = queryParams.length > 0 ? `?${queryParams.slice(1)}` : '';

        return formattedQueryParams;
    }

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

    const handleDateChange = (key, date) => {
        setValidationErrors({ startDate: '', endDate: '' });
        if (key === startDate && date != null && selectedItems.endDate != null && new Date(selectedItems.endDate) < date) {
            setValidationErrors({ startDate: 'Start date cannot be after end date', endDate: '' });

        } else if (key === endDate && date && selectedItems.startDate && new Date(selectedItems.startDate) > date) {
            setValidationErrors({ startDate: '', endDate: 'End date cannot be before start date' });
        }
        else{
            setSelectedItems(prevState => {
                const dateString = formatDate(date);
    
                if (prevState[key] === dateString) {
                    return {
                        ...prevState,
                        [key]: formatDate(new Date()),
                    };
                } else {
                    return {
                        ...prevState,
                        [key]: dateString,
                    };
                }
            });
        }        
        
    };

    const handlePriceChange = (key, price) => {
        setSelectedItems(prevState => {
            return {
                ...prevState,
                [key]: price,
            };
        });
    }

    const createString = (name, value) => {
        if (!value || value == [] || (Array.isArray(value) && value.length == 0))
            return '';

        if (!Array.isArray(value))
            return `&${name}=${value}`;

        if (Array.isArray(value) && value.length > 0)
            return `&${name}=${value.join(',')}`;

    };

    const formatDate = (date) => {
        return date.toISOString().split('T')[0];
    }

    const checkDate = (date) => {
        if(date == formatDate(new Date())){
            return null;
        }
        return date;
    }

    const checkPrice = (price) => {
        if(price == 0)
            return null;

        return price;
    }

    const handleResetFilter = () => {
        setSelectedItems({
            employees: [],
            registers: [],
            locations: [],
            startDate: new Date().toISOString().split('T')[0],
            endDate: new Date().toISOString().split('T')[0],
            price: 0
        });
    };

    const sortData = (data) => {
        return [...data].sort((a, b) => new Date(a.date) - new Date(b.date));
    };

    const extractData = (data) => {
        return [...new Set(data.map(item => item))];
    };

    const handleClick = (receiptId) => {
        setSelectedSale(data.find(x => x.receiptId === receiptId));
        setDetailsModal(true);
    };

   

    const handlePrintReport = () => {
     //todo   
     console.log(selectedItems);
    }
    

    return (
        <Container className="d-flex flex-column align-items-center ">
            <h1>All Sales</h1>
            <Container className="d-flex">
                {selectedSale && <ModalSaleDetails setShow={setDetailsModal} show={detailsModal} sale={selectedSale} />}
                <Container className="d-flex flex-column align-items-center" style={{ width: '40%' }} >
                    <h4>Select filter criteria</h4>
                    <Row style={{ alignItems: 'center', margin: '2px' }}>
                        <Col>
                            Select start date:
                        </Col>
                        <Col>
                            <DatePicker selected={selectedItems.startDate} onChange={(date) => handleDateChange(startDate, date)} />
                            {validationErrors.startDate && <div style={{ color: 'red', fontSize: '12px' }}>{validationErrors.startDate}</div>}
                        </Col>
                    </Row>
                    <Row style={{ alignItems: 'center', margin: '2px' }}>
                        <Col>
                            Select end date:
                        </Col>
                        <Col>
                            <DatePicker selected={selectedItems.endDate} onChange={(date) => handleDateChange(endDate, date)} />
                            {validationErrors.endDate && <div style={{ color: 'red', fontSize: '12px' }}>{validationErrors.endDate}</div>}
                        </Col>
                    </Row>
                    {user.role === 'Admin' && <Dropdown>
                        <Dropdown.Toggle variant="white" id="dropdown-employee" className="w-2">
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
                        <Dropdown.Toggle variant="white" id="dropdown-register" className="w-2">
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
                        <Dropdown.Toggle variant="white" id="dropdown-location" className="w-2">
                            Select Location
                        </Dropdown.Toggle>
                        <Dropdown.Menu style={{ alignItems: 'center' }}>
                            {locations.map((option) => (
                                <div key={option} style={{ marginLeft: '10px' }}>
                                    <Form.Check
                                        type="checkbox"
                                        label={option}
                                        style={{ margin: 'auto' }}
                                        checked={selectedItems.locations.includes(option)}
                                        onChange={() => handleCheckboxChange('locations', option)} />
                                </div>
                            ))}

                        </Dropdown.Menu>
                    </Dropdown>
                    <Form>
                        <Form.Group>
                            <Row >
                                <Col>Select max total price:</Col>
                                <Col xs="5">
                                    <Form.Control
                                        value={selectedItems.price}
                                        type="number"
                                        presicion={1}
                                        onChange={(e) => handlePriceChange('price', e.target.value)} />
                                </Col>
                            </Row>
                        </Form.Group>
                    </Form>

                    <Container className="d-flex flex-column align-items-center p-3" >                        
                        <Button variant="dark" style={{ width: '70%', margin: '2px' }} onClick={handleResetFilter}>Reset filter</Button>
                        <Button variant="outline-dark" style={{ width: '70%', margin: '2px' }} onClick={handlePrintReport}>Print report</Button>
                    </Container>
                </Container>

                {data && data.length > 0 ?
                    <Container>
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
                    : <Container>
                        {selectedItems.employees == [] && selectedItems.dates == [] && selectedItems.registers == []
                            ? <h3>There have been no sales yet!</h3>
                            : <h3>There are no sales with that criteria!</h3>}
                    </Container>}
            </Container>

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
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';

function AppHeader() {
  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand as={Link} to="/">Predmet projekat</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/login">Login</Nav.Link>
            <Nav.Link as={Link} to="/register">Register</Nav.Link>
            <Nav.Link as={Link} to="/admins">Admins</Nav.Link>
            <Nav.Link as={Link} to="/employees">Employees</Nav.Link>
            <Nav.Link as={Link} to="/products">Products</Nav.Link>
            <Nav.Link as={Link} to="/categories">Categories</Nav.Link>
            <Nav.Link as={Link} to="#order">Order</Nav.Link>
            <Nav.Link as={Link} to="#sell">Sell</Nav.Link>
            <NavDropdown title="Dropdown" id="basic-nav-dropdown">
              <NavDropdown.Item as={Link} to="/account">My account</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item as={Link} to="#historyofsales">History of sales</NavDropdown.Item>
              <NavDropdown.Item as={Link} to="#finances">Finances</NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}

export default AppHeader;
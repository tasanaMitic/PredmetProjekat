import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import AppHeader from './components/Header';
import ProductsPage from './components/pages/ProductsPage';
import HomePage from './components/pages/HomePage';
import AccountPage from './components/pages/AccountPage'
import OrderPage from './components/pages/OrderPage';
import EmployeesPage from './components/pages/EmployeesPage';
import LoginPage from './components/pages/LoginPage';
import RegisterPage from './components/pages/RegisterPage';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

function App() {
  return (
    <Router>
      <div className="App">
        <header id="header">
          <AppHeader></AppHeader>
        </header>
        <main id="main">
          <Switch>
            <Route exact path="/">
              <HomePage></HomePage>
            </Route>
            <Route exact path="/login">
              <LoginPage></LoginPage>
            </Route>
            <Route exact path="/register">
              <RegisterPage></RegisterPage>
            </Route>
            <Route path="/products">
              <ProductsPage></ProductsPage>
            </Route>
            <Route path="/account">
              <AccountPage></AccountPage>
            </Route>
            <Route path="/order">
              <OrderPage></OrderPage>
            </Route>
            <Route path="/employees">
              <EmployeesPage></EmployeesPage>
            </Route>
          </Switch>
        </main>
      </div>
    </Router>
  );
}

export default App;

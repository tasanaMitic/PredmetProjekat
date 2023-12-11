import PropTypes from 'prop-types';

const HomePage = ({ user }) => {

    return (
        user ? 
        <div> 
            <h1>Welcome to home page</h1>
        </div>
        : <div>
            <h1>Welcome. Please login.</h1>
        </div>
    );
}

HomePage.propTypes = {
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default HomePage;
import Alert from 'react-bootstrap/Alert';

function AlertDissmisable({error, setError}) {  //todo constants
  //console.log(error.response.data);
  //console.log(error.response.data);

    return (
      <Alert variant="danger" onClose={() => setError(false)} dismissible>
        <Alert.Heading>Oh snap!  {error.response && error.response.data.title}</Alert.Heading>
        <p>
          {error.response ? error.response.data.title : "Something went wrong! Try again shortly!"}  
        </p>
      </Alert>
    );
  
}

export default AlertDissmisable;
import Alert from 'react-bootstrap/Alert';

function AlertDissmisable({error, setError}) {  //todo constants
  const renderSwitch = (param) => {
    switch(param){
      case 400:
        return "Bad requst was sent to the server. Please check your inputs.";
      case 401:
        return "You are not authorized to perform this request."
      default:
        return "Something went wrong."
    }
  }
  

    return (
      <Alert variant="danger" onClose={() => setError(false)} dismissible>
        <Alert.Heading>Oh snap!  {error.response && error.response.data.title}</Alert.Heading>
        <p>
          {error.response ? renderSwitch(error.response.status) : "Something went wrong! Try again shortly!"}  
        </p>
      </Alert>
    );
  
}

export default AlertDissmisable;
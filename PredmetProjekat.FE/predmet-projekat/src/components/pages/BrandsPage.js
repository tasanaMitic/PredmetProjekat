import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import BrandsTable from '../BrandsAndCategoriesTable'
import { getBrands } from '../../api/methods'

const BrandsPage = () => {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getBrands().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
        .then(data => {
                setData(data);
                setIsPending(false);
                setError(null);
            })
            .catch(err => {
                setIsPending(false);
                setError(err);
            })
    }, []);

    


    return (
        <Container>        
            <h1>Brands</h1>
            {error && <div>{error}</div>}
            {!isPending && <BrandsTable brands={data} />}
        </Container>
    );
}

export default BrandsPage;
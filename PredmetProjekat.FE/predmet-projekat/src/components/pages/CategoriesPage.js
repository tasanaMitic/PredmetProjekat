import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import CategoriesTable from '../BrandsAndCategoriesTable'
import { getCategories } from '../../api/methods'

const CategoriesPage = () => {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getCategories().then(res => {
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
            <h1>Categories</h1>
            {error && <div>{error}</div>}
            {!isPending && <CategoriesTable categories={data} />}
        </Container>
    );
}

export default CategoriesPage;
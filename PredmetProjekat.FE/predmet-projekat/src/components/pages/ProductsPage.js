import { Button, Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import ProductTable from "../ProductTable";
import AlertDissmisable from "../Alert";

const ProductsPage = () => {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    const [allProductsAreShown, setAllProductsAreShown] = useState(false);
    const [stockedProductsAreShown, setStockedProductsAreShown] = useState(false);

    // data
    const [allProducts, setAllProducts] = useState(null);
    const [stockedProducts, setStockedroducts] = useState(null);

    const handleDelete = (productId) => {
        console.log('Delete : ' + productId);
    }

    useEffect(() => {

        //initial set of data here
        setAllProducts([
            { name: 'dress', size: 'M', season: 'spring', sex: 'F', productId: 123 },
            { name: 't-shirt', size: 'S', season: 'summer', sex: 'F', productId: 456 },
            { name: 'jacket', size: 'L', season: 'winter', sex: 'F', productId: 789 },
        ]);
        setStockedroducts([
            { name: 'dress', size: 'M', season: 'spring', sex: 'F', productId: 123, quantity: 6 },
            { name: 't-shirt', size: 'S', season: 'summer', sex: 'F', productId: 456, quantity: 7 },
            { name: 'jacket', size: 'L', season: 'winter', sex: 'F', productId: 789, quantity: 8 },
        ]);

        //dont change state here - infinity loop
    }, []);

    return (
        <Container>
            <h1>Products</h1>
            {error && <AlertDissmisable error={error} setError={setError}/>}
            {!error && <Button onClick={() => setStockedProductsAreShown(current => !current)}>Na stanju</Button>}
            {!error && stockedProductsAreShown && stockedProducts &&
                <ProductTable products={stockedProducts}
                    handleDelete={handleDelete} />}
            {!error && <Button onClick={() => setAllProductsAreShown(current => !current)}>Prikaži sve proizvode</Button>}
            {!error && allProductsAreShown && allProducts &&
                <ProductTable products={allProducts}
                    handleDelete={handleDelete} />}
        </Container>
    );
}

export default ProductsPage;
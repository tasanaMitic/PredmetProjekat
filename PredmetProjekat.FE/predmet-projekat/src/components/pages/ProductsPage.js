import { Button, Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import ProductTable from "../ProductTable";
import AlertDissmisable from "../Alert";

function ProductsPage() {
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    const [allProductsAreShown, setAllProductsAreShown] = useState(false);
    const [stockedProductsAreShown, setStockedProductsAreShown] = useState(false);

    // data
    const [allProducts, setAllProducts] = useState(null);
    const [stockedProducts, setStockedroducts] = useState(null);

    const handleClick = () => {
        console.log(allProducts);
    }

    const handleDelete = (productId) => {
        console.log('Delete : ' + productId);
    }

    useEffect(() => {
        console.log('useEffect ran');

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
            <h1>Product page component</h1>
            {isPending && <div>Loading..</div>}
            {error && <AlertDissmisable />}
            {!error && <Button onClick={() => setStockedProductsAreShown(current => !current)}>Na stanju</Button>}
            {!error && stockedProductsAreShown && stockedProducts &&
                <ProductTable products={stockedProducts}
                    handleDelete={handleDelete} />}
            {!error && <Button onClick={() => setAllProductsAreShown(current => !current)}>Prikaži sve proizvode</Button>}
            {!error && allProductsAreShown && allProducts &&
                <ProductTable products={allProducts}
                    handleDelete={handleDelete} />}
            <Button onClick={handleClick}>Click</Button>
        </Container>
    );
}

export default ProductsPage;
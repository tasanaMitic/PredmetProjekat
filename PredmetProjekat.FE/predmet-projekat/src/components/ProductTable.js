
import { Button, Table } from "react-bootstrap";

const ProductTable = ({ products, handleDelete }) => {

    const handleClick = (productId) => {
        console.log(products);
    }

    return (
        <Table striped hover>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Product name</th>
                    <th>Season</th>
                    <th>Sex</th>
                    <th>Size</th>
                    {products.filter(e => e.quantity).length > 0 ? <th>Quantity</th> : null}
                </tr>
            </thead>
            <tbody>
                {products.map((product) => (
                    <tr key={product.productId}>
                        <td>{product.productId}</td>
                        <td>{product.name}</td>
                        <td>{product.season}</td>
                        <td>{product.sex}</td>
                        <td>{product.size}</td>
                        {products.filter(e => e.quantity).length > 0 ? <td>{product.quantity}</td> : null}
                        <td>
                            <Button variant="dark" onClick={() => handleClick(product.productId)}>Edit</Button>
                            <Button variant="dark" onClick={() => handleDelete(product.productId)}>Delete</Button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>
    );
}

export default ProductTable;
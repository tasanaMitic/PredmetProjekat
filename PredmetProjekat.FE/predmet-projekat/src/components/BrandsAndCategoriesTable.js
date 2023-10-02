import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import ModalCheck from './ModalCheck'

const BrandsAndCategoriesTable = ({ categories, brands }) => {
    const [check, setCheck] = useState(false);
    const [data, setData] = useState(null);

    const confirmDelete = () => {
        console.log('confirm delete');
    }

    useEffect(() => {
        if (categories) {
            setData(categories);
        }
        else if (brands) {
            setData(brands);
        }
    }, []);
    return (
        <Container>
            {data && <Table striped hover>
                <ModalCheck setShow={setCheck} show={check} confirm={confirmDelete} />
                <thead>
                    <tr>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    { categories && data.map((categorie) => (
                        <tr key={categorie.categoryId}>
                            <td>{categorie.name}</td>
                                <td>
                                    <Button variant="dark" onClick={() => setCheck(true)}>Delete</Button>
                                </td>
                            
                        </tr>
                    ))}
                    { brands && data.map((brand) => (
                        <tr key={brand.brandId}>
                            <td>{brand.name}</td>
                                <td>
                                    <Button variant="dark" onClick={() => setCheck(true)}>Delete</Button>
                                </td>
                            
                        </tr>
                    ))}
                </tbody>
            </Table>
            }
        </Container>
    );
}

export default BrandsAndCategoriesTable;
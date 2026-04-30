import type { IProduct } from "../model/IProduct";
import Product from "./Product";

interface Props {
  products: IProduct[];
  addProduct: () => void;
}

export default function ProductList({products, addProduct}: Props){

    return (
    <>
    <h2>Product List</h2>
     
     {products.map((p: IProduct) =>(
      <Product key={p.id} products={p} />

    ) )}
      <button onClick={addProduct}>Add Product</button>
    </>
  );
}
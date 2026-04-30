import type { IProduct } from "../model/IProduct";

interface Props {
  products: IProduct;
}

export default function Product({products}: Props){
  return (
    <>
    {products.isActive ?(
    <div>
    <h3>{products.name}</h3>
    <p>{products.price}</p>
    </div>
    ) : <h3>{products.name} Tükendi</h3>}
    </>
 );
}
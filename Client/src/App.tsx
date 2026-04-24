import { useState } from "react"

function App() {
  return (
    <>
      <Header />
      <ProductList />
    </>
  
)
}

function Header() {
  return (
    <h1>Header</h1>
  )
}

function ProductList(){

  const [products, setProducts] = useState([
    {id: 1, name: "Product 1", price: 10, is_active: true},
    {id: 2, name: "Product 2", price: 20, is_active: false},
    {id: 3, name: "Product 3", price: 30, is_active: true},
  ])

    function addProduct(){
      const newProduct = {id: 4, name: "Product 4", price: 40, is_active: true}
      setProducts([...products, newProduct])
    }

  return (
    <>
    <h2>Product List</h2>
     
     {products.map(p =>(
      <Product key={p.id} products={p} />

    ) )}
      <button onClick={addProduct}>Add Product</button>
    </>
  );
}

function Product(props: any){
  return (
    <>
    {props.products.is_active ?(
    <div>
    <h3>{props.products.name}</h3>
    <p>{props.products.price}</p>
    </div>
    ) : <h3>{props.products.name} Tükendi</h3>}
    </>
 );
}


export default App

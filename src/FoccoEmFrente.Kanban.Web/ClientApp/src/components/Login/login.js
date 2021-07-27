import React from "react";
function Botao({onVoltar}){
   return (
      <>
      <button className="btn btn-primary" type="submit">Entrar</button>
      <button className="btn btn-secondary" onClick={onVoltar}>Voltar</button>
      </>
   )
}
function Senha(){
   return (
      <>
      <label htmlFor="Senha">Senha</label>
      <input id="senha" type="password" placeholder ="Senha"></input>
           
      </>
   )

}
function Email(){

   return (
      <>
      <label htmlFor="email">Email</label>
      <input id="email" type="email" placeholder ="E-mail"></input>
             
      </>
   )
}
function Paragrafo(){
   return <p>Bem vindo ao <strong>Sunday.com</strong>, melhor sistema para gestao de tasks</p>

}
function Div(){}
function Macro(variavel){
   return(<>

      <div style = {{width: "450px"}}>
               <form>
                  {variavel}
               </form>
            </div>
         </>
         )

}
export default function Login({history}) {
   
   const onVoltar = () =>
   {
      history.push("/");
   }

   return (
      <div>
         <Macro variavel={Email}/>
      </div>
      
   
   );
}
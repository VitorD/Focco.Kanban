import React from "react";

function ShowScreen({onVoltar})
{
  return(
   <>
      <p>Crie uma conta no <strong>Sunday.com</strong></p>
         <form>
            <label htmlFor="email">Email</label>
            <input id="email" type="email" placeholder ="E-mail"></input>
            <label htmlFor="Senha">Senha</label>
            <input id="senha" type="password" placeholder ="Senha"></input>
            <label htmlFor="confirm-password">Confirmar a senha</label>
            <input id="confirm-password" type="password" placeholder ="Confirmar a Senha"></input>
            <button className="btn btn-primary" type="submit">Registrar</button>
            <button className="btn btn-secondary" onClick={onVoltar}>Voltar</button>

         </form> 
   </>
      )
}


export default function Register({history}) {

   const onVoltar = () =>
   {
      history.push("/login");
   }

   return (
      <div style = {{width: "450px"}}>
         <ShowScreen onVoltar = {onVoltar}/>
      </div>
   );
}
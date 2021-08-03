import React,{ useState} from "react";

function ShowScreen({onVoltar})
{

   const [email,setEmail] = useState('');
   const [confirmPassword,setConfirmPassword] = useState('');
   const [password,setPassword] = useState('');

   const onRegister = async (event) => {
      event.preventDefault();
    const response = await fetch("api/account/register",
            {
               method:"POST",
               headers:{
                  "Content-Type":"application/json",
                  "Accept":"application/json"
               },
               body:JSON.stringify({
                  email: email,
                  password: password,
                  confirmPassword: confirmPassword
               })
            }
      );

      const responseContent = await response.json();
      if(!response.ok)
      {
         window.alert(responseContent);
         return;
      }

      localStorage.setItem("token",responseContent);
      window.alert("sucesso");
      console.log("response",response);
   };
  return(
   <>
      <p>Crie uma conta no <strong>Sunday.com</strong></p>
         <form onSubmit={onRegister}>
            <label htmlFor="email">Email</label>
            <input id="email" type="email" placeholder ="E-mail" value={email} onChange={(event) => setEmail(event.target.value)}></input>
            <label htmlFor="Senha">Senha</label>
            <input id="senha" type="password" placeholder ="Senha" value={password} onChange={(event) => setPassword(event.target.value)}></input>
            <label htmlFor="confirm-password">Confirmar a senha</label>
            <input id="confirm-password" type="password" placeholder ="Confirmar a Senha" value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)}></input>
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
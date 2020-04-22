<?php

// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idUser = "";
// =========== INPUT
$name=$_POST["name"];
$pwd=$_POST["pwd"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
if(!$conn){
	 echo("connessione non riuscita <br />");	
}

$sqlUsers = "SELECT * FROM Users WHERE Name='" .$name. "' AND Pwd='" .$pwd."';";
//$sqlUsers = "SELECT * FROM Users WHERE Name='Pippo' AND Psw='1234'";
$resultUsers = mysqli_query($conn,$sqlUsers);
if(mysqli_num_rows($resultUsers) > 0){
	while($row = mysqli_fetch_assoc($resultUsers)){
		echo "ID@".$row['Id'] . "|NAME@".$row['Name'] . "|PSW@".$row['Psw'] . "|TOTCASH@".$row['TotCash'] .";";
	}
}else{
echo("Non trovato User <br />");	
}


?>
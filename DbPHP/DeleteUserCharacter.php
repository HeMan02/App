<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idCharacter = $_POST["idCharacter"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
echo("prova connessione <br />");
if(!$conn){
	 echo("connessione non riuscita <br />");	
}
// ============ RETURN CHARACTERS
$sql = "delete FROM Characters WHERE Id=" .$idCharacter;
//$sql = "SELECT * FROM Characters WHERE IdCharacter=" .$idCharacter;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
?>
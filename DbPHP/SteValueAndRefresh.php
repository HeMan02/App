<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$id = $_POST["id"];
$idUser = $_POST["idUser"];
$valueRelance = $_POST["valueRelance"];
$valueTotCashUser = $_POST["valueTotCashUser"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
echo("prova connessione <br />");
if(!$conn){
	 echo("connessione non riuscita <br />");	
}
// ============ UPDATE CHARACTER VALUE RELANCE
$sql = "UPDATE Characters SET Price=" .$valueRelance. " WHERE id =" .$id;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
// ============ UPDATE CHARACTER ID RELANCE
$sql = "UPDATE Characters SET IdRelance=" .$idUser. " WHERE id =" .$id;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
// ============ UPDATE USER COINS
$sql = "UPDATE Users SET TotCash=" .$valueTotCashUser. " WHERE id =" .$idUser;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
// ============ GET DATA FOR REFRESH
$sql = "SELECT * FROM Characters WHERE Id=" .$id;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
echo(mysqli_num_rows($result));
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "NOME@".$row['Nome'] . "|LIVELLO@".$row['Livello'] . "|SKILLBONUS@".$row['SkillBonus'] . "|SKILLMALUS@".$row['SkillMalus'] ."|SKILLRANDOM@".$row['SkillRandom']  ."|TYPE@".$row['Type']."|HEAD@".$row['Head'] ."|BODY@".$row['Body']  ."|DATA@".$row['DataCreazione'] . "|PRICE@".$row['Price'] ."|LIFE@".$row['Life'] ."|ID@".$row['Id'] .";";
	}
}
?>
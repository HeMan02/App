<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idUser = $_POST["idUser"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
echo("prova connessione <br />");
if(!$conn){
	 echo("connessione non riuscita <br />");	
}
// ============ RETURN CHARACTERS
$sql = "SELECT * FROM Characters WHERE IdUser=" .$idUser;
//$sql = "SELECT * FROM Characters ";
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
echo(mysqli_num_rows($result));
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "NOME@".$row['Nome'] . "|LIVELLO@".$row['Livello'] . "|SKILLBONUS@".$row['SkillBonus'] . "|SKILLMALUS@".$row['SkillMalus'] ."|SKILLRANDOM@".$row['SkillRandom']  ."|TYPE@".$row['Type']."|HEAD@".$row['Head'] ."|BODY@".$row['Body']  ."|DATA@".$row['DataCreazione'] . "|PRICE@".$row['Price'] ."|LIFE@".$row['Life'] ."|ID@".$row['Id']."|DATASTEP@".$row['DataStep']."|IDDUNGEON@".$row['IdDungeon'].";";
	}
}
?>
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
$sql = "SELECT * FROM Dungeon";
//$sql = "SELECT * FROM Dungeon WHERE IdUser=1";
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
echo(mysqli_num_rows($result));
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "IDDUNGEON@".$row['IdDungeon'] . "|TIME@".$row['Time'] . "|DESCRIPTION@".$row['Description'] . "|TYPE@".$row['Type'] ."|CASHWIN@".$row['CashWin']  ."|LEVEL@".$row['Level'].";";
	}
}
?>
<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idDungeon = $_POST["idDungeon"];
$idCharacter = $_POST["idCharacter"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
//echo("prova connessione con valori idDungeon: " .$idDungeon. " idCharacter " .$idCharacter);
echo("prova connessione");
if(!$conn){
	 echo("connessione non riuscita " );	
}
// ============ SET DUNGEON ON CHARACTERS
$sql = "update Characters set IdDungeon=" .$idDungeon. " WHERE Id=" .$idCharacter;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
// ============ SET DATA END OCCUPED
$sql = "select Time from Dungeon WHERE IdDungeon=" .$idDungeon;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
while($row = $result->fetch_assoc()){
$dateStep = $row['Time'];
}
//========== DATE NOW
$datetimeToAddStep = new DateTime;
echo 'prima' . $datetimeToAddStep->format('Y-m-d:H:i:s') . '<br />';
//========== DATE DB
$datetimeToAddStep->modify('+ ' . $dateStep . ' hour');
echo 'dopo' . $datetimeToAddStep->format('Y-m-d:H:i:s') . '<br />';
$stringDate = $datetimeToAddStep->format('Y-m-d H:i:s');
$sql = "update Characters set EndOccuped='$stringDate' WHERE Id=" .$idCharacter;
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
?>
<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idUser = $_POST["idUser"];
// =========== CLASS DUNGEON
class Dungeon{
  public $idDungeon;
  public  $time;
  public  $description;
  public  $type;
  public  $cashWin;
  public  $level;
  public  $name;
  public  $goal; 
  public  $place;
  public  $dateEnd;
  public function __construct($idDungeon,$time,$description,$type,$cashWin,$level,$name,$goal,$place,$dateEnd) {
    $this->IdDungeon = $idDungeon;
    $this->Time = $time;
    $this->Description = $description;
    $this->Type = $type;
    $this->CashWin = $cashWin;
    $this->Level = $level;
    $this->Name = $name;
    $this->Goal = $goal;
    $this->Place = $place;
  }
}
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
//echo("prova connessione <br />");
if(!$conn){
	 echo("connessione non riuscita <br />");	
}
// ============ CHECK DATA END
$sqlCheckDataEnd = "SELECT IdDungeon FROM Dungeon where TIMEDIFF(DataEnd, now()) < 0";
$resultCheckDataEnd = mysqli_query($conn,$sqlCheckDataEnd);
$resultCheckDataEnd = $conn->query($sqlCheckDataEnd);
echo(mysqli_num_rows($resultCheckDataEnd));
if(mysqli_num_rows($resultCheckDataEnd) > 0){
    $countDeleteDungeon = 0;
	while($rowCheckDataEnd = mysqli_fetch_assoc($resultCheckDataEnd)){
		$sqlDeleteDataEnd= "delete FROM Dungeon where IdDungeon=" .$rowCheckDataEnd['IdDungeon']. ";";
        $resultDeleteDataEnd = mysqli_query($conn,$sqlDeleteDataEnd);
		$resultDeleteDataEnd = $conn->query($sqlDeleteDataEnd);
        $countDeleteDungeon = $countDeleteDungeon +1;
        echo $countDeleteDungeon;
	}
    // ========== CREATE DUNGEON RANDOM
    for ($i = 0; $i < $countDeleteDungeon; $i++) {
        $rabdDungeon = GenerateRandomDungeon();
    	$sqlCreateDungeon = "INSERT INTO  Dungeon (IdDungeon,Time,Description,Type,CashWin,Level,Name,Goal,Place,DataEnd) VALUES(" .$rabdDungeon->IdDungeon. "," .$rabdDungeon->Time. "," .$rabdDungeon->Description. "," .$rabdDungeon->Type. "," .$rabdDungeon->CashWin. "," .$rabdDungeon->Level. "," .$rabdDungeon->Name. "," .$rabdDungeon->Goal. "," .$rabdDungeon->Place. ",DATE_ADD(NOW(), INTERVAL " .rand(1,7). " DAY))";
    	//$sqlCreateDungeon = "INSERT INTO  Dungeon (IdDungeon,Time,Description,Type,CashWin,Level,Name,Goal,Place,DataEnd) VALUES(" .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). "," .rand(0,6). ",NOW())";
        $resultCreateDungeon = mysqli_query($conn,$sqlCreateDungeon);
        echo(" AGGIUNTO ");
	}
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
function GenerateRandomDungeon(){
  	$randDungeon = new Dungeon(rand(1,100),rand(3,24),rand(0,10),rand(0,5),rand(10,100),rand(1,3),rand(0,4),rand(0,8),rand(0,9));
    return $randDungeon;
}
?>
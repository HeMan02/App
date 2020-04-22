<?php
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$randomName=$_POST["name"];
$idUser=$_POST["idUser"];

class Character{
  public $nome;
  public  $livello;
  public  $skillBonus;
  public  $skillMalus;
  public  $skillRandom;
  public  $type;
  public  $head;
  public  $body; 
  public  $DataStep;
  public function __construct($nome,$livello,$skillBonus,$skillMalus,$skillRandom,$type,$head,$body,$DataStep) {
    $this->nome = $nome;
    $this->livello = $livello;
    $this->skillBonus = $skillBonus;
    $this->skillMalus = $skillMalus;
    $this->skillRandom = $skillRandom;
    $this->type = $type;
    $this->head = $head;
    $this->body = $body;
    $this->DataStep = $DataStep;
  }
}
// ========== VERIFICA CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
if(!$conn){
	 echo("connessione non riuscita <br />");
}
// ============ VERIFICA SCADENZA ASTE CHARACTERS
$sql = "update Characters as Cha,( SELECT Id from Characters  where (datediff(DataCreazione + 1,now())) < 0 and IdUser = 0) as Chb   set Cha.IdUser = IdRelance where Cha.Id = Chb.Id";
$result = mysqli_query($conn,$sql);
$sql = "delete from `Characters` as Cha where Cha.Id = (select Id from `Characters` as Chb where Chb.IdUser = 0) and ( datediff(date_sub(Cha.DataCreazione,interval Cha.DataStep day),now() ) < 0 )";
$result = mysqli_query($conn,$sql);
// echo "<pre>" ,print_r($result, TRUE),"</pre>";
// ============ VERIFICA TABELLA PIENA O VUOTA
// Seleione dàtà più recente di DataCreazione dei chàràcters
$sql = "SELECT Nome,DataCreazione,DataStep FROM Characters WHERE IdUser = 0 ORDER BY DataCreazione DESC LIMIT 1";
$result = mysqli_query($conn,$sql);
if(mysqli_num_rows($result)==0) {
		echo("NO DATA");
     $myCharacter = GenerateRandomCharacter($randomName);
     $sql = "INSERT INTO  Characters (IdUser,Id,Nome,Livello,SkillBonus,SkillMalus,	SkillRandom,Type,Head,Body,DataCreazione,DataStep,Price,Life,IdRelance,IdDungeon,EndOccuped) VALUES(0,'" .rand(1,100). "','" .$myCharacter->nome. "'," .$myCharacter->livello. "," .$myCharacter->skillBonus. "," .$myCharacter->skyllMalus. "," .$myCharacter->skillRandom. "," .$myCharacter->type. "," .$myCharacter->head. "," .$myCharacter->body. ",NOW()," .$myCharacter->DataStep. ",0,100,0,0,NOW())";
     $result = mysqli_query($conn,$sql);
      die('Could not get data: ' . mysql_error());
}
// ciclo tutti i risultàti
while($row = $result->fetch_assoc()){
//========== DTE NOW
$datetimeNow = new DateTime;
echo $datetimeNow->format('Y-m-d:H:i:s') . '<br />';
//========== DTE DB
$datetimeDB = new DateTime($row['DataCreazione']);
echo $datetimeDB->format('Y-m-d:H:i:s') . '<br />';
$datetimeDB->modify('+ ' . $row['DataStep'] . ' hour');
echo $datetimeDB->format('Y-m-d:H:i:s') . '<br />';
//=========== DIFF DATE
$interval = $datetimeNow->diff($datetimeDB);
echo $interval->format('%R');
//=========== CHECK NEW OBJ
if(strcmp($interval->format('%R'),'-')==0){
	echo("Negàtivo e istanzio nuovo oggetto");
    $myCharacter = GenerateRandomCharacter($randomName);
    $sql = "INSERT INTO  Characters (IdUser,Id,Nome,Livello,SkillBonus,SkillMalus,	SkillRandom,Type,Head,Body,DataCreazione,DataStep,Price,Life,IdRelance,IdDungeon,EndOccuped) VALUES(0,'" .rand(1,100). "','" .$myCharacter->nome. "'," .$myCharacter->livello. "," .$myCharacter->skillBonus. "," .$myCharacter->skyllMalus. "," .$myCharacter->skillRandom. "," .$myCharacter->type. "," .$myCharacter->head. "," .$myCharacter->body. ",NOW()," .$myCharacter->DataStep. ",0,100,0,0,NOW())";
    $result = mysqli_query($conn,$sql);
}else{
	echo("Positivo Return");
}
//============== TEST OUTPUT
}

function GenerateRandomCharacter($randomName){
  	$myCharacter = new Character($randomName,1,rand(0,7),rand(0,8),rand(0,6),rand(0,9),rand(0,18),rand(0,27),rand(1,24));
    return $myCharacter;
}
?>
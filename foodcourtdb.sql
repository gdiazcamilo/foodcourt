 
 C R E A T E   D A T A B A S E   [ f o o d c o u r t ]  go
  U S E   [ f o o d c o u r t ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ D i s h ]         S c r i p t   D a t e :   1 0 / 2 7 / 2 0 1 5   6 : 5 0 : 4 9   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ D i s h ] (  
 	 [ I d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ N a m e ]   [ v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ D e s c r i p t i o n ]   [ v a r c h a r ] ( 1 5 0 )   N U L L ,  
 	 [ P r i c e ]   [ f l o a t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ D i s h ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ I d ]   A S C  
 ) W I T H   ( P A D _ I N D E X   =   O F F ,   S T A T I S T I C S _ N O R E C O M P U T E   =   O F F ,   I G N O R E _ D U P _ K E Y   =   O F F ,   A L L O W _ R O W _ L O C K S   =   O N ,   A L L O W _ P A G E _ L O C K S   =   O N )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ O r d e r ]         S c r i p t   D a t e :   1 0 / 2 7 / 2 0 1 5   6 : 5 0 : 4 9   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ O r d e r ] (  
 	 [ I d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ U s e r N a m e ]   [ v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ D i s h I d ]   [ i n t ]   N O T   N U L L ,  
 	 [ S t a t e ]   [ i n t ]   N U L L   C O N S T R A I N T   [ D F _ O r d e r _ S t a t e ]     D E F A U L T   ( ( 0 ) ) ,  
   C O N S T R A I N T   [ P K _ O r d e r ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ I d ]   A S C  
 ) W I T H   ( P A D _ I N D E X   =   O F F ,   S T A T I S T I C S _ N O R E C O M P U T E   =   O F F ,   I G N O R E _ D U P _ K E Y   =   O F F ,   A L L O W _ R O W _ L O C K S   =   O N ,   A L L O W _ P A G E _ L O C K S   =   O N )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ O r d e r ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ O r d e r _ D i s h ]   F O R E I G N   K E Y ( [ D i s h I d ] )  
 R E F E R E N C E S   [ d b o ] . [ D i s h ]   ( [ I d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ O r d e r ]   C H E C K   C O N S T R A I N T   [ F K _ O r d e r _ D i s h ]  
 G O  
 U S E   [ m a s t e r ]  
 G O  
 A L T E R   D A T A B A S E   [ f o o d c o u r t ]   S E T     R E A D _ W R I T E    
 G O  
 

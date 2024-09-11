USE veiculos

create table tb_cadastrov(    
ID_VEICULO INT IDENTITY PRIMARY KEY,
N_CHASSI VARCHAR(17),      
MONTADORA VARCHAR(50),                  
NOME_VEICULO VARCHAR(100),              
ANO_FABRICACAO INT,                   
COR VARCHAR(30),                      
PLACA VARCHAR(8),                 
VALOR_MERCADO DECIMAL(10, 2), 
FOTO VARCHAR(255)
);

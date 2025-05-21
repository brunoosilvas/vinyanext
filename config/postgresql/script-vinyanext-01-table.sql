\c vinyanext

CREATE TABLE public.gsis_usuario (
	id serial4 NOT NULL,
	cpf varchar(11) NULL,
	senha text NULL,
	CONSTRAINT gsis_usuario_pk PRIMARY KEY (id)
);
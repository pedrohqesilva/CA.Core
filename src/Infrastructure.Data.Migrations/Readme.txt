Para gerar a migração
add-migration migration_nomeDaMigracao -Project Core.Infrastructure.Data.Migrations

Para gerar a seed
add-migration seed_nomeDoSeed -Project Core.Infrastructure.Data.Migrations

Para executar as migrações

Gerar script
Script-migration -Project Core.Infrastructure.Data.Migrations

Atualizar direto no Banco
Update-Database -Project Core.Infrastructure.Data.Migrations
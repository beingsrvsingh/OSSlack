Control Panel → Credential Manager → Windows Credentials


cmd > cmdkey /list

## ENVIRONMENT - 
MACOS - DEV_KEYCHAIN_NAME | DEV_KEYCHAIN_PASSWORD
WINDOW|LINUX = DEV_KEY_NAME

## MACOS-COMMANDS
security find-generic-password -s jwtSigningKey -w app.keychain-db

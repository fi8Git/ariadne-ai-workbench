# Security Policy

## Reporting A Vulnerability

Please do not open a public issue for exploitable vulnerabilities, secrets, tokens, sensitive datasets or private user data.

Use GitHub private vulnerability reporting if it is enabled for this repository. If private vulnerability reporting is not available yet, contact the maintainers through a private channel and include only the minimum information needed to start triage. If no private channel is listed, open a public issue that only asks for a private reporting path, without exploit details or sensitive data.

When reporting a vulnerability, include:

- affected version, commit or branch;
- a short description of the impact;
- safe reproduction steps that do not include secrets or private data;
- whether local project files, reports or datasets could be exposed.

## Local-First Data Handling

Ariadne AI Workbench is local-first. User datasets are expected to stay on the user's machine by default.

Do not attach user datasets, generated reports containing sensitive data, SQLite databases, local project folders, API keys, tokens or credentials to public issues, pull requests, discussions or logs.

If a sample is needed to reproduce a security issue, use a small synthetic dataset that contains no personal, confidential or proprietary information.

## Supported Versions

Ariadne is currently in a pre-MVP foundation phase. Security fixes are handled on the `main` branch until formal releases and supported version ranges exist.

## GitHub Security Features

Maintainers should enable repository-level security features in GitHub settings, including code scanning, secret scanning, push protection, Dependabot alerts and Dependabot security updates.

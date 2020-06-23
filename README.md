![CI Build](https://github.com/paulhannon0/BeltsAndLeaders.Server/workflows/CI%20Build/badge.svg?branch=master)

# BeltsAndLeaders.Server

This repository contains the source code for the API for the 'Belts and Leaders Programme' application being developed by Sage. The purpose of the application is to record and rank the achievements of security champions.

## Installation and Usage

1. Clone the repository from GitHub (`git@github.com:paulhannon0/BeltsAndLeaders.Server.git`)
2. Navigate to the root directory on the command line and bring up the database using `docker-compose up -d` (**note:** this takes around 30 seconds)
3. Run the service from VSCode under `Run -> Start (With/Without) Debugging` (**note:** this method uses the environment variables found in .vscode/launch.json)

## Project Documents

- API architecture document: https://docs.google.com/document/d/1xAYon9WhJmmrLa8_9wPnu8TBl96-SBiknQQhZcbaQak/edit?usp=sharing
- Entity relationship diagram: https://drive.google.com/file/d/1_xkvzZJJ9XBV-zCytR7QonwzfgVCm_Yn/view?usp=sharing

## Project Structure

### BeltsAndLeaders.Server.Api

This layer contains:
- Main application host
- Configuration
- Controllers
- Extensions
- Middleware
- Models

### BeltsAndLeaders.Server.Business

This layer contains:
- Commands
- Queries
- Models

### BeltsAndLeaders.Server.Common

This layer contains:
- Exceptions

### BeltsAndLeaders.Server.Data

This layer contains:
- Migrations
- Models
- Repositories

### BeltsAndLeaders.Server.Tests

This layer contains:
- Common steps
- Configuration
- Endpoint integration tests
- Helpers


import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import BbqSessions from './components/BbqSessions';

const queryClient = new QueryClient(); 